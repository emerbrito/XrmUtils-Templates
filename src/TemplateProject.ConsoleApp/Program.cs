using Common.Logging;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Tooling.CrmConnectControl;
using PowerArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XrmCodeGen.LoginWindow;

namespace $safeprojectname$
{
    class Program
    {

        CrmConnectionManager _connMgr;
        CrmServiceClient _crmSvc;
        ILog log;

        [STAThread]
        static void Main(string[] args)
        {

            var logger = LogManager.GetLogger<Program>();
            
            try
            {

                var commands = Args.Parse<CommandArguments>(args);
                var app = new Program(logger);

                bool success = false;

                if (string.IsNullOrWhiteSpace(commands.Connection))
                {
                    logger.Trace("Interactive login.");
                    success = app.DoInteractiveLogin();
                }
                else
                {
                    logger.Trace("Non interactive login.");
                    success = app.DoNonInteractiveLogin(commands.Connection);
                }

                if (success)
                {
                    logger.Info($"Connected to: {app._crmSvc.ConnectedOrgFriendlyName}");

                    var ep = new EntryPoint(app._crmSvc, LogManager.GetLogger<EntryPoint>());
                    logger.Info($"Entering {nameof(ep.Execute)}() method of {nameof(EntryPoint)}.");
                    ep.Execute();

                }
                else
                {
                    logger.Info("Unable to Connect. Try again.");
                }

            }
            catch (ArgException ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<CommandArguments>());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex); ;
            }

            Console.WriteLine();
            Console.WriteLine("Done processing. Press any key to exit.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();

        }

        public Program(ILog logger)
        {

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            log = logger;

        }

        private bool DoNonInteractiveLogin(string connString)
        {

            log.Trace($"Entered {nameof(DoNonInteractiveLogin)}()");

            bool flag = false;

            try
            {
                _crmSvc = new CrmServiceClient(connString);
            }
            catch (Exception ex)
            {

                throw new Exception($"Unable to instantiate {nameof(CrmServiceClient)}. Please check the connection string and try again. Inner exception: {ex.Message}", ex);
            }

            if (_crmSvc.IsReady)
            {
                flag = true;
                log.Trace($"{nameof(_crmSvc)}.IsReady = true");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_crmSvc.LastCrmError))
                {
                    log.Error(_crmSvc.LastCrmError);
                }
                if (_crmSvc.LastCrmException != null)
                {
                    log.Error(_crmSvc.LastCrmException.Message, _crmSvc.LastCrmException);
                }
            }

            log.Trace($"Exiting {nameof(DoNonInteractiveLogin)}()");

            return flag;
        }

        private bool DoInteractiveLogin()
        {

            log.Trace($"Entered {nameof(DoInteractiveLogin)}()");

            bool flag = false;
            _connMgr = null;

            var loginDlg = new CrmLogin()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            loginDlg.ConnectionToCrmCompleted += LoginDlg_ConnectionToCrmCompleted;

            loginDlg.ShowDialog();

            loginDlg.ConnectionToCrmCompleted -= LoginDlg_ConnectionToCrmCompleted;

            try
            {

                if (loginDlg.CrmConnectionMgr != null
                    && loginDlg.CrmConnectionMgr.CrmSvc != null
                    && loginDlg.CrmConnectionMgr.CrmSvc.IsReady)
                {

                    _connMgr = loginDlg.CrmConnectionMgr;
                    _crmSvc = _connMgr.CrmSvc;

                    flag = true;
                    log.Trace($"{nameof(_crmSvc)}.IsReady = true");

                }
                else
                {
                    if (loginDlg.CrmConnectionMgr != null)
                    {
                        if (!string.IsNullOrWhiteSpace(loginDlg.CrmConnectionMgr.LastError))
                        {
                            log.Error(loginDlg.CrmConnectionMgr.LastError);
                        }
                        if (loginDlg.CrmConnectionMgr.LastException != null)
                        {
                            log.Error(loginDlg.CrmConnectionMgr.LastException.Message, loginDlg.CrmConnectionMgr.LastException);
                        }
                    }
                }

            }
            catch (Exception exception)
            {

                log.Error($"Unexpected error: {exception.Message}", exception);
                flag = false;

            }

            log.Trace($"Exiting {nameof(DoInteractiveLogin)}()");
            return flag;
        }

        private void LoginDlg_ConnectionToCrmCompleted(object sender, EventArgs e)
        {

            log.Trace($"Entered {nameof(LoginDlg_ConnectionToCrmCompleted)}()");

            if (sender != null && sender is CrmLogin)
            {
                ((CrmLogin)sender).Close();
            }

            log.Trace($"Exiting {nameof(LoginDlg_ConnectionToCrmCompleted)}()");

        }

    }
}

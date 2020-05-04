using System.Linq;
using ElectronNET.API;
using Microsoft.AspNetCore.Mvc;
using NihFix.PgProfiler.LogProcessing;

namespace NihFix.PgProfiler.Ui.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PgProfilerDataController: ControllerBase
    {
        private LogChangeTracker _logChangeTracker;
        public PgProfilerDataController()
        {
            _logChangeTracker=new LogChangeTracker(@"D:\postgres\data\log");
            Electron.IpcMain.On("subscribeToProfilerData", (l) =>
            {
                _logChangeTracker.OnLogChange += (sender, args) =>
                    Electron.IpcMain.Send(Electron.WindowManager.BrowserWindows.First(), "profileData", args.NewData);
            });
        }
        
    }
}
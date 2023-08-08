using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Data;
using System.Data.SqlClient;


Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

PatternLayout patternLayout = new PatternLayout();
patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
patternLayout.ActivateOptions();

RollingFileAppender roller = new RollingFileAppender();
roller.AppendToFile = false;
roller.File = @"Logs\EventLog.txt";
roller.Layout = patternLayout;
roller.MaxSizeRollBackups = 5;
roller.MaximumFileSize = "1GB";
roller.RollingStyle = RollingFileAppender.RollingMode.Size;
roller.StaticLogFileName = true;
roller.ActivateOptions();
hierarchy.Root.AddAppender(roller);

MemoryAppender memory = new MemoryAppender();
memory.ActivateOptions();
hierarchy.Root.AddAppender(memory);

hierarchy.Root.Level = Level.Info;
hierarchy.Configured = true;
log4net.Config.BasicConfigurator.Configure(hierarchy);

ILog log = log4net.LogManager.GetLogger(typeof(Program));
const string connectionString = @"data source=.\SQLEXPRESS;database=AdventureWorks;user id=sa;pwd=1234.";
log.Warn(String.Format("Connection String :{0}", connectionString));
log.Info(String.Format("Bağlantı durumu : {0}", "Bağlanamadı"));
log.Debug(String.Format("Finally bloğundayız. Bağlantı durumu {0}", "Bağlandı"));
log.Info("Program sonu");
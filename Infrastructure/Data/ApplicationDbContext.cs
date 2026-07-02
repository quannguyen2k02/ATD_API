using Domain.Enitties;
using Domain.Enitties.ChangeLog;
using Domain.Enitties.IO;
using Domain.Enitties.LCD;
using Domain.Enitties.LED;
using Domain.Entities.LED;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Line> Lines { get; set; }
        /// <summary>
        /// LED DB Set
        /// </summary>
        public DbSet<LED> LEDs { get; set; }
        public DbSet<LedModel> LEDModels { get; set; }
        public DbSet<LedModelConfig> ledModelConfigs { get; set; }
        public DbSet<LedConfig> LedConfigs { get; set; }
        public DbSet<LedCamera> LedCameras { get; set; }
        public DbSet<LedStatus> LedStatuses { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<LedDeviceStatus> LedDeviceStatuses { get; set; }
        public DbSet<LedResult> LedResults { get; set; }

        ///> <summary>
        ///LCD DB Set
        ///
        public DbSet<LCD> LCDs { get; set; }
        public DbSet<LCDModel> LCDModels { get; set; }
        public DbSet<LCDConfig> LCDConfigs { get; set; }
        public DbSet<LCDResult> LCDResults { get; set; }
        ///> <summary>
        ///Log change Set
        ///
        public DbSet<LEDLog> LEDLogs { get; set; }

        ///<summary>
        ///IO DB Set
        /// </summary>
        public DbSet<IO> IOs { get; set; }
        public DbSet<IOConfig> IOConfigs { get; set; }
        public DbSet<IOConfigManagement> IOConfigManagements { get; set; }
        public DbSet<IOModel> IOModels { get; set; }
        public DbSet<MotionPoint> MotionPoints { get; set; }
        public DbSet<MotionPointsManagement> MotionPointsManagements { get; set; }
        public DbSet<Offset> Offsets { get; set; }
        public DbSet<OffsetManagement> OffsetManagements { get; set; }
    }
}

using Domain.Enitties;
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

        ///> <summary>
        ///LCD DB Set
        ///
        public DbSet<LCD> LCDs { get; set; }
        public DbSet<LCDModel> LCDModels { get; set; }
        public DbSet<LCDConfig> LCDConfigs { get; set; }
        public DbSet<LCDResult> LCDResults { get; set; }

    }
}

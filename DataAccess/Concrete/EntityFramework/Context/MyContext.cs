using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class MyContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public MyContext()
        {

        }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=boostteam2.database.windows.net;Database=DreamDB;User Id=BoostTeam2;Password=Cimbom.1905");
        }

        public virtual DbSet<Break> Breaks { get; set; }
        public virtual DbSet<Eventt> Events { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<Debit> Debits { get; set; }
        public virtual DbSet<Bounty> Bounties { get; set; }
        public virtual DbSet<UserBountyDto>  UserBountyDtos { get; set; }

        public virtual DbSet<Vacation> Vacations{ get; set; }
      
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<UserCostDto> UserCostDtos { get; set; }
        public virtual DbSet<UserDebitDto> UserDebitDtos { get; set; }
        public virtual DbSet<UserVacationDto> UserVacationDtos { get; set; }

        public virtual DbSet<UserShiftBreakDto> UserShiftBreakDtos { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<UserShiftDto> UserShiftDtos { get; set; }

        public virtual DbSet<CompanySectorDto> CompanySectorDtos { get; set; }
        public virtual DbSet<PersonnelDocuments> PersonelDocuments { get; set; }
    }




    }


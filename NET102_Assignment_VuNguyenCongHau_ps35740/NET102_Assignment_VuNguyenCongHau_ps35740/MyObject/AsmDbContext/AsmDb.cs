using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;

namespace NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject.TableObj
{
    public class AsmDb : DbContext
    {
        public AsmDb(DbConnection conn, bool contextOwnsConnection)
            : base(conn, contextOwnsConnection)
        {
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
    }
}
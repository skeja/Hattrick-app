using System;
using Microsoft.EntityFrameworkCore;
using hattrick_full.Models;

namespace hattrick_full.Models {
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }


}


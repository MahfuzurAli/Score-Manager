using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCOREgrp05.Models;

namespace SCOREgrp05.Data
{
    public class MBContext : DbContext
    {
        public MBContext (DbContextOptions<MBContext> options)
            : base(options)
        {
        }

        public DbSet<SCOREgrp05.Models.Match> Match { get; set; } = default!;
        public DbSet<SCOREgrp05.Models.But> But { get; set; } = default!;
    }
}

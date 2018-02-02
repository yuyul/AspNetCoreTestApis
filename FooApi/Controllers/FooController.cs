using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FooApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("mypolicy")]
    public class FooController
        :ControllerBase
    {
        private readonly FooContext _context;

        public FooController(FooContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var foo = await _context.Foo.FindAsync(id);

            if (foo != null)
            {
                return Ok(foo);
            }

            return NotFound();
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(Foo foo)
        {
            await _context.Foo.AddAsync(foo);

            await _context.SaveChangesAsync();

            return CreatedAtRoute(nameof(FooController.Get), new { id = foo.Id }); 
        }
    }

    public class FooContext
        : DbContext
    {
        public FooContext(DbContextOptions<FooContext> options) : base(options) { }

        public DbSet<Foo> Foo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Foo>()
                .Property(f => f.Id)
                .ForSqlServerUseSequenceHiLo();

            modelBuilder.Entity<Foo>()
                .Property(f => f.Bar)
                .IsRequired();
        }
    }

    public class Foo
    {
        public int Id { get; set; }
        public string Bar { get; set; }
    }
}

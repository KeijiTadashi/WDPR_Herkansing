using api.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api;

public class StichtingContext : IdentityDbContext<Gebruiker, IdentityRole, string>
{
    public StichtingContext(DbContextOptions<StichtingContext> options) : base(options)
    {
    }
// public StichtingContext(DbContextOptions<StichtingContext> options)
    //     : base(options)
    // {
    // }

    public DbSet<Test> Tests { get; set; }
    public DbSet<Aandoening> Aandoeningen { get; set; }
    public DbSet<Ervaringsdeskundige> Ervaringsdeskundigen { get; set; }
    public DbSet<Bedrijf> Bedrijven { get; set; }
    public DbSet<Beheerder> Beheerders { get; set; }
    public DbSet<Benadering> Benaderingen { get; set; }
    public DbSet<Beperking> Beperkingen { get; set; }
    public DbSet<Hulpmiddel> Hulpmiddelen { get; set; }
    public DbSet<Onderzoek> Onderzoeken { get; set; }
    public DbSet<OnderzoeksType> OnderzoeksTypes { get; set; }
    public DbSet<Verzorger> Verzorgers { get; set; }
    public DbSet<Gebruiker> Gebruikers { get; set; } = default!;
    public DbSet<OpdrachtRespons> Opdracht {get;set;};
}
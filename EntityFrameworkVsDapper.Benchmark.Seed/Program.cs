// See https://aka.ms/new-console-template for more information
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;

const int ENTITY_MAX = 10000;

BenchmarkDbContext context = new BenchmarkDbContext();

context.Database.EnsureCreated();

Random random= new Random();

int materialCount = context.Materials.Count();
if (materialCount <= ENTITY_MAX)
{
    List<Materials> materials = new List<Materials>();
    for (int i = materialCount; i < ENTITY_MAX; i++)
    {
        materials.Add(new Materials
        {
            Name = "Matirial " + i,
            Description = "Description " + i,
            Cost = random.Next(1, 1000),
            CreatedDateUtc = DateTime.UtcNow
        });
    }
    context.Materials.AddRange(materials);
    context.SaveChanges();
}

int brandCount = context.Brands.Count();
if (brandCount <= ENTITY_MAX)
{
    List<Brands> brands = new List<Brands>();
    for (int i = brandCount; i < ENTITY_MAX; i++)
    {
        brands.Add(new Brands
        {
            Name = "Brand " + i,
            Description = "Description " + i,
            CreatedDateUtc = DateTime.UtcNow
        });
    }
    context.Brands.AddRange(brands);
    context.SaveChanges();
}

int styleCount = context.Styles.Count();
if (styleCount <= ENTITY_MAX)
{
    List<Styles> styles = new List<Styles>();
    for (int i = styleCount; i < ENTITY_MAX; i++)
    {
        styles.Add(new Styles
        {
            BrandId = random.Next(1, 1000),
            Name = "Brand " + i,
            Description = "Description " + i,
            CreatedDateUtc = DateTime.UtcNow
        });
    }
    context.Styles.AddRange(styles);
    context.SaveChanges();
}

int benchCount = context.Benches.Count();
if (benchCount <= ENTITY_MAX)
{
    List<Benches> benches = new List<Benches>();
    for (int i = benchCount; i < ENTITY_MAX; i++)
    {
        benches.Add(new Benches
        {
            MaterialId = random.Next(1, 1000),
            StyleId = random.Next(1, 1000),
            Name = "Brand " + i,
            Description = "Description " + i,
            Cost = random.Next(4, 1000),
            Height= random.Next(4, 300),
            Width= random.Next(4, 300),
            Depth=random.Next(4, 300),
            CreatedDateUtc = DateTime.UtcNow
        });
    }
    context.Benches.AddRange(benches);
    context.SaveChanges();
}
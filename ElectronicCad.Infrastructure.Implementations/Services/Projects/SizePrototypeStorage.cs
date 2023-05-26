using ElectronicCad.Infrastructure.Abstractions.Models.Projects;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;

namespace ElectronicCad.Infrastructure.Implementations.Services.Projects;

/// <summary>
/// Implementation size prototype storage.
/// </summary>
public class SizePrototypeStorage : ISizePrototypesStorage
{
    private readonly static IEnumerable<SizePrototype> systemPrototypes;

    /// <summary>
    /// Static constructor.
    /// </summary>
    static SizePrototypeStorage()
    {
        var prototypes = new List<SizePrototype>
        {
            new SizePrototype("iPhone 14", 390, 844),
            new SizePrototype("iPhone 14 Pro", 393, 852),
            new SizePrototype("iPhone 14 Plus", 428, 926),
            new SizePrototype("iPhone 14 Pro Max", 430, 932),
            new SizePrototype("iPhone 13 Pro Max", 428, 926),
            new SizePrototype("iPhone 13 / 13 Pro", 390, 844),
            new SizePrototype("iPhone 13 mini", 375, 812),
            new SizePrototype("iPhone SE", 320, 568),
            new SizePrototype("iPhone 8 Plus", 414, 736),
            new SizePrototype("iPhone 8", 375, 667),

            new SizePrototype("Android Small", 360, 640),
            new SizePrototype("Android Large", 360, 800),

            new SizePrototype("MacBook Air", 1280, 832),
            new SizePrototype("MacBook Pro 14", 1512, 982),
            new SizePrototype("MacBook Pro 16", 1728, 1117),
            new SizePrototype("Desktop", 1440, 1024),
            new SizePrototype("Wireframe", 1440, 1024),
            new SizePrototype("TV", 1280, 720),

            new SizePrototype("Slide 16:9", 1920, 1080),
            new SizePrototype("Slide 4:3", 1024, 768),
            
            new SizePrototype("A4", 595, 842),
            new SizePrototype("A5", 420, 595),
            new SizePrototype("A6", 297, 420),
            new SizePrototype("Letter", 612, 792),
            new SizePrototype("Tabloid", 792, 1224)
        };

        systemPrototypes = prototypes;
    }

    /// <inhertidoc />
    public IEnumerable<SizePrototype> GetPrototypes()
    {
        return systemPrototypes;
    }

    /// <inhertidoc />
    public void AddPrototype(SizePrototype prototype)
    {
        throw new NotImplementedException();
    }

    /// <inhertidoc />
    public void RemovePrototype(SizePrototype prototype)
    {
        throw new NotImplementedException();
    }
}
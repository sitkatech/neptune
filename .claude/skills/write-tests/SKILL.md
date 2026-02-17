---
name: write-tests
description: Create unit tests for API controllers (MSTest) or Angular components (Jasmine). Follows project testing patterns and conventions.
allowed-tools: [Read, Glob, Grep, Edit, Write, Bash(dotnet test:*), Bash(npm test:*)]
---

# Testing Patterns

Load this skill when creating unit tests for API controllers or Angular components.

## Cross-References

| Testing... | Also load |
|------------|-----------|
| API controllers | `api-patterns.md` (for controller conventions) |
| Angular components | `angular-patterns.md` (for component conventions) |

---

## MSTest API Tests

Create tests in `Neptune.Tests` project.

### Controller Test Template

```csharp
[TestClass]
public class EntityControllerTests
{
    private NeptuneDbContext _dbContext;
    private EntityController _controller;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<NeptuneDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new NeptuneDbContext(options);

        var neptuneConfiguration = Options.Create(new NeptuneConfiguration());
        _controller = new EntityController(
            _dbContext,
            Mock.Of<ILogger<EntityController>>(),
            neptuneConfiguration
        );
    }

    [TestMethod]
    public async Task List_ReturnsAllEntities()
    {
        // Arrange
        _dbContext.Entities.Add(new Entity { Name = "Test" });
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _controller.List();

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result.Result;
        var entities = (List<EntityGridDto>)okResult.Value;
        Assert.AreEqual(1, entities.Count);
    }

    [TestMethod]
    public async Task GetByID_ReturnsEntity_WhenExists()
    {
        // Arrange
        var entity = new Entity { EntityID = 1, Name = "Test" };
        _dbContext.Entities.Add(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _controller.GetByID(1);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task GetByID_ReturnsNotFound_WhenNotExists()
    {
        // Act
        var result = await _controller.GetByID(999);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }
}
```

### Test Naming Convention

`{MethodName}_{ExpectedBehavior}_{Condition}`

Examples:
- `List_ReturnsAllEntities`
- `GetByID_ReturnsEntity_WhenExists`
- `GetByID_ReturnsNotFound_WhenNotExists`
- `Create_ReturnsCreatedEntity_WhenValid`
- `Update_ReturnsNoContent_WhenSuccessful`

### ApprovalTests Pattern

Neptune uses ApprovalTests for snapshot-based testing. Approved output files use `.approved.txt` extension:

```csharp
[TestMethod]
public void SomeOutput_MatchesApproved()
{
    var result = SomeMethodThatProducesOutput();
    Approvals.Verify(result);
}
```

When test output changes, update the `.approved.txt` file to match.

### Controller Constructor Note

Neptune controllers take 3 parameters — `NeptuneDbContext`, `ILogger<T>`, and `IOptions<NeptuneConfiguration>`. Make sure your test setup mocks all three:

```csharp
var neptuneConfiguration = Options.Create(new NeptuneConfiguration());
var controller = new EntityController(
    dbContext,
    Mock.Of<ILogger<EntityController>>(),
    neptuneConfiguration
);
```

---

## Jasmine Angular Tests

Component tests in `*.spec.ts` files alongside components.

### Component Test Template

```typescript
describe('EntityDetailComponent', () => {
  let component: EntityDetailComponent;
  let fixture: ComponentFixture<EntityDetailComponent>;
  let entityServiceSpy: jasmine.SpyObj<EntityService>;

  beforeEach(async () => {
    entityServiceSpy = jasmine.createSpyObj('EntityService', ['getByID']);

    await TestBed.configureTestingModule({
      imports: [EntityDetailComponent],
      providers: [
        { provide: EntityService, useValue: entityServiceSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(EntityDetailComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load entity when entityID is set', fakeAsync(() => {
    const mockEntity = { entityID: 1, name: 'Test Entity' };
    entityServiceSpy.getByID.and.returnValue(of(mockEntity));

    component.entityID = '1';
    tick();

    component.entity$.subscribe(entity => {
      expect(entity).toEqual(mockEntity);
    });

    expect(entityServiceSpy.getByID).toHaveBeenCalledWith(1);
  }));

  it('should display entity name in template', fakeAsync(() => {
    const mockEntity = { entityID: 1, name: 'Test Entity' };
    entityServiceSpy.getByID.and.returnValue(of(mockEntity));

    component.entityID = '1';
    fixture.detectChanges();
    tick();
    fixture.detectChanges();

    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.card-title').textContent).toContain('Test Entity');
  }));
});
```

### Service Mocking Pattern

```typescript
// Create spy with methods
const serviceSpy = jasmine.createSpyObj('ServiceName', ['method1', 'method2']);

// Configure return values
serviceSpy.method1.and.returnValue(of(mockData));
serviceSpy.method2.and.returnValue(throwError(() => new Error('Test error')));

// Provide in TestBed
providers: [{ provide: ServiceName, useValue: serviceSpy }]
```

---

## Running Tests

### API Tests
```powershell
# Run all tests
dotnet test Neptune.Tests/Neptune.Tests.csproj

# Run specific test by name
dotnet test Neptune.Tests/Neptune.Tests.csproj --filter "FullyQualifiedName~SomeTestName"

# Run tests with coverage
dotnet test Neptune.Tests/Neptune.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

### Angular Tests
```powershell
cd Neptune.Web
npm test           # Run tests once
```

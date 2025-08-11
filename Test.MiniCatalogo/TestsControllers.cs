using api.MiniCatalogo;
using api.MiniCatalogo.Config.Const;
using api.MiniCatalogo.DTOs.Request;
using api.MiniCatalogo.DTOs.Response;
using api.MiniCatalogo.Model;
using api.MiniCatalogo.Model.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Test.MiniCatalogo.Seed;

namespace Testes;

public class Test : WebApplicationFactory<Program>
{
    public IConfiguration Configuration { get; private set; }
    public Test()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        Configuration = builder.Build();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services)=>
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });
        });
    }
}

public class TestsControllers : IClassFixture<Test>, IDisposable
{
    readonly HttpClient _client;
    readonly PopulateDb _populateDb;
    string _categoriRout = "/api/Categori";
    string _productRout = "/api/Product";
    IDbContextFactory<EntityContext> _factori;
    Categoria _categoriStandard = new Categoria
            {
                Nome = "Categoria teste",
                Produtos = new List<Produto>
                {
                    new Produto
                    {
                        Nome = "Produto teste",
                    }
                }
            };
    public TestsControllers(Test factory)
    {
        _factori = factory.Services.GetService<IDbContextFactory<EntityContext>>()!;
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _populateDb = new PopulateDb(_factori);
        
        _populateDb.Create_Categori(_categoriStandard).GetAwaiter()
              .GetResult();

    }
    #region [Categori]
    [Fact]
    public async Task Categori_GetAll_Returns_Ok()
    {
        var response = await _client.GetAsync(_categoriRout);

        var body = await Return_Body<IEnumerable<CategoriaResponseDTO>>(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(_categoriStandard.Nome, body.First().Nome);
        Assert.Equal(_categoriStandard.Id, body.First().Id);
    }

    [Fact]
    public async Task Categori_Create_Returns_Ok()
    {
        var categoria = new CategoriaResponseDTO
        {
            Nome = "Integration Test",
        };

        var postResponse = await _client.PostAsJsonAsync(_categoriRout, categoria);

        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
    }

    [Fact]
    public async Task Categori_Returns_Error_Duplicate()
    {
        var categoria = new CategoriaResponseDTO
        {
            Nome = "Integration Test",
        };

        await _populateDb.Create_Categori(new Categoria
        {
            Nome = categoria.Nome
        });

        var postResponse = await _client.PostAsJsonAsync(_categoriRout, categoria);

        var body = await Return_Body<ExcepitioResponse>(postResponse);

        Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        Assert.Equal(Messages._categoriExist, body.Detail);
    }
    #endregion

    #region [Product]
    [Fact]
    public async Task Product_Get_Returns_Ok()
    {
        var response = await _client.GetAsync(_productRout+$"?Page={1}&Size={10}&CategoriaId={_categoriStandard.Id}");

        var body = await Return_Body<IEnumerable<ProdutoResponseDTO>>(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(_categoriStandard.Produtos.First().Nome, body.First().Nome);
        Assert.Equal(_categoriStandard.Produtos.First().Id, body.First().Id);
    }

    [Fact]
    public async Task Product_Get_Pagination_Empity_Returns_Ok()
    {
        var response = await _client.GetAsync(_productRout + $"?Page={2}&Size={10}&CategoriaId={_categoriStandard.Id}");

        var body = await Return_Body<IEnumerable<ProdutoResponseDTO>>(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.Count() == 0);
    }

    [Fact]
    public async Task Product_Create_Returns_Ok()
    {
        var categoria = new ProdutoRequestDTO
        {
            Nome = "Produto integration test",
            Preco = 10.99M,
            CategoriaId = _categoriStandard.Id
        };

        var postResponse = await _client.PostAsJsonAsync(_productRout, categoria);

        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
    }

    [Fact]
    public async Task Product_Returns_Error_Duplicate()
    {
        var produto = new ProdutoRequestDTO
        {
            Nome = "Produto integration test",
            Preco = 10.99M,
            CategoriaId = _categoriStandard.Id
        };

        await _populateDb.Create_Product(new Produto
        {
            Nome = produto.Nome,
            Preco = produto.Preco,
            CategoriaId = produto.CategoriaId
        });

        var postResponse = await _client.PostAsJsonAsync(_productRout, produto);

        var body = await Return_Body<ExcepitioResponse>(postResponse);

        Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        Assert.Equal(Messages._nameProductExistInCategori, body.Detail);
    }

    [Fact]
    public async Task Product_Returns_Error_Creat_Price_Zero()
    {
        var produto = new ProdutoRequestDTO
        {
            Nome = "Produto integration test",
            Preco = 0.0M,
            CategoriaId = _categoriStandard.Id
        };

        var postResponse = await _client.PostAsJsonAsync(_productRout, produto);

        var body = await Return_Body<ExcepitioResponse>(postResponse);

        Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        Assert.True(body.Errors!.ContainsKey("preco"));
    }
    #endregion

    async Task<T> Return_Body<T>(HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();
        
        return System.Text.Json.JsonSerializer.Deserialize<T>(body, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public void Dispose()
    {
        _client.Dispose();
        _populateDb.Dispose();
    }
}

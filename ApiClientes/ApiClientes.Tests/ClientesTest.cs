using ApiClientes.Services.Models;
using ApiClientes.Services.Models.Requests;
using ApiClientes.Services.Models.Responses;
using ApiClientes.Tests.Helpers;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiClientes.Tests
{
    public class ClientesTest
    {
        private const string? endpoint = "api/Clientes";

        [Fact]
        public ClientesResponse Test_Clientes_Post_Returns_Created()
        {
            var faker = new Faker("pt_BR");
            var request = new ClientesCreateRequest
            {
                Nome = faker.Person.FullName.Replace(".", string.Empty),
                Email = faker.Person.Email,
                Telefone = faker.Phone.PhoneNumber("(##)#########"),
                Cpf = faker.Person.Cpf(false)
            };

            var response = TestHelper.CreateClient.PostAsync
                (endpoint, TestHelper.CreateContent(request)).Result;

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            var result = TestHelper.ReadContent<ClientesResponse>(response);
            result.Status.Should().Be(201);
            result.Mensagem.Should().Be("Cliente cadastrado com sucesso.");
            result.Cliente.Should().NotBeNull();

            return result;
        }

        [Fact]
        public void Test_Clientes_Put_Returns_Ok()
        {
            var resultCadastro = Test_Clientes_Post_Returns_Created();

            var faker = new Faker("pt_BR");
            var request = new ClientesUpdateRequest
            {
                Id = resultCadastro.Cliente?.Id,
                Nome = faker.Person.FullName.Replace(".", string.Empty),
                Email = faker.Person.Email,
                Telefone = faker.Phone.PhoneNumber("(##)#########")
            };

            var response = TestHelper.CreateClient.PutAsync
                (endpoint, TestHelper.CreateContent(request)).Result;

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            var result = TestHelper.ReadContent<ClientesResponse>(response);
            result.Status.Should().Be(200);
            result.Mensagem.Should().Be("Cliente atualizado com sucesso.");
            result.Cliente.Should().NotBeNull();
        }

        [Fact]
        public void Test_Clientes_Put_Returns_NotFound()
        {
            var faker = new Faker("pt_BR");
            var request = new ClientesUpdateRequest
            {
                Id = Guid.NewGuid(),
                Nome = faker.Person.FullName.Replace(".", string.Empty),
                Email = faker.Person.Email,
                Telefone = faker.Phone.PhoneNumber("(##)#########")
            };

            var response = TestHelper.CreateClient.PutAsync
                (endpoint, TestHelper.CreateContent(request)).Result;

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.NotFound);

            var result = TestHelper.ReadContent<ClientesResponse>(response);
            result.Status.Should().Be(404);
            result.Mensagem.Should().Be("Cliente não encontrado, verifique o ID informado.");
            result.Cliente.Should().BeNull();
        }

        [Fact]
        public void Test_Clientes_Delete_Returns_Ok()
        {
            var resultCadastro = Test_Clientes_Post_Returns_Created();

            var response = TestHelper.CreateClient.DeleteAsync
                (endpoint + "/" + resultCadastro.Cliente?.Id).Result;

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            var result = TestHelper.ReadContent<ClientesResponse>(response);
            result.Status.Should().Be(200);
            result.Mensagem.Should().Be("Cliente excluído com sucesso.");
            result.Cliente.Should().NotBeNull();
        }

        [Fact]
        public void Test_Clientes_Delete_Returns_NotFound()
        {
            var response = TestHelper.CreateClient.DeleteAsync
                (endpoint + "/" + Guid.NewGuid()).Result;

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.NotFound);

            var result = TestHelper.ReadContent<ClientesResponse>(response);
            result.Status.Should().Be(404);
            result.Mensagem.Should().Be("Cliente não encontrado, verifique o ID informado.");
            result.Cliente.Should().BeNull();
        }

        [Fact]
        public void Test_Clientes_GetAll_Returns_Ok()
        {
            var resultCadastro = Test_Clientes_Post_Returns_Created();
            var response = TestHelper.CreateClient.GetAsync(endpoint).Result;

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            var result = TestHelper.ReadContent<List<ClientesViewModel>>(response);

            result.FirstOrDefault(c => c.Id == resultCadastro.Cliente?.Id)
                .Should().NotBeNull();
        }

        [Fact]
        public void Test_Clientes_GetById_Returns_Ok()
        {
            var resultCadastro = Test_Clientes_Post_Returns_Created();
            var response = TestHelper.CreateClient.GetAsync
                (endpoint + "/" + resultCadastro.Cliente?.Id).Result;

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            var result = TestHelper.ReadContent<ClientesViewModel>(response);

            result.Id.Should().Be(resultCadastro.Cliente?.Id);
            result.Nome.Should().Be(resultCadastro.Cliente?.Nome);
            result.Email.Should().Be(resultCadastro.Cliente?.Email);
            result.Telefone.Should().Be(resultCadastro.Cliente?.Telefone);
            result.Cpf.Should().Be(resultCadastro.Cliente?.Cpf);
        }
    }
}

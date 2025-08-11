## Como rodar o projeto
Tenha o .net 8 instalado.

### 1. Clone o repositório
```bash
	git clone https://github.com/EduardoVSR1995/api.MiniCatalogo.git
```

### 2. Navegue até o diretório do projeto
```bash	
	cd api.MiniCatalogo
```	

### 3. Restaure as dependências
```bash	
	dotnet restore
```

### 4. Execute o projeto apartir da solução .sln
```bash	
	dotnet run --project ./api.MiniCatalogo
```

### 5. Acesse a API
	cesse a URL `http://localhost:5001/swagger/index.html` no seu navegador para ver a documentação da API e testar os endpoints.

### 6. Executando os testes apartir da solução o .sln
Para executar os testes, você pode usar o seguinte comando:
```bash
	dotnet test
```

### Resolução
Foi utilizado o Entity Framework Core com SQLite para persistência de dados e agilizar o desenvolvimento, swagger para documentção da API de forma simples e clara, testes com xUnit para testes de integração pois possui capacidades para testes de integração e unitarios(apesar de não utilizar testes unitarios aqui).
Utilizando uma arquitetura simples e aproximada a arquitetura de camadas.
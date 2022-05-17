## API RestFull para processo seletivo da Único
Essa API foi desenvolvida em ASP .NET Core versão 6 utilizando o Visual Studio 2022 Community e banco de dados Relacional SQL Server Express (Todos versões gratuitas)
No desenvolvimento foram utilizados EntityFramewerk 6.0 para persistência e consutal dos dados e o XUnit .NET para testes unitários.
Para servidor do SQL Server foi utilizado uma imagem Docker da propria Microsoft.
O projeto está estruturas da seguinte forma:

├── PROJETO\
│   ├── FeiraSP.WEB.API\ 
│   └── FeiraSP.Web.API.TEST \
│   └── FeiraSP.WEB.API.sln
├── SCRIPTS\
│   └── 01 - CREATE DATABASE.sql
│   └── 02 - CREATE SCHEMA.sql
│   └── 03 - INSERT DISTRITOS.sql
│   └── 04 - INSERT TB_SUB_PREFEITURA.sql
│   └── 05 - INSERT TB_FEIRA.sql
│   └── 99 - ROLLBACK.sql


# Pasta Projeto
Nessa pasta contém todos os fontes para execução da aplicação, bem como o projeto de teste unutáro. 
Os projetos .NET possui uma ferramenta integrada que é possível acessar todos os arquivos em um único lugar. 
Para abrir o projeto, basta acessar o Arquivo de Soluções (.sln) localizado em PROJETO\FeiraSP.WEB.API.sln

# Pasta SCRIPTS
Nessa pasta todos os arquivos necessários para criar e popular a base de dados para todar o projeto. Para ter sucesso na instalação, é necessário seguir a ordem numérica:
* 01 - Cria a base de dados com o nome `DB_FEIRA_LIVRE`
* 02 - Cria três tabelas responsável pelo armazenamento das informações: 
  1. _TB_DISTRITO_ - Informações do Distrito da feira.
  2. _TB_SUB_PREFEITURA_ - Informações da SubPrefeitura onde onde a feira acontece.
  3. _TB_FEIRA_ - Todas as informações referente a feira de rua.
* 03 - Realiza a Carga dos dados referente a tabela de Distritos
* 04 - Realiza a Carga dos dados referente a tabela da SubPrefeitura
* 05 - Realiza a Carga dos dados referente a tabela de Feira
* 09 - Esse script só deverá ser executado em caso de falha ou desistalação da aplicação. Ele remove a base de dados do servidor SQL



# Configuração

Instalação do servidor Docker:

Rodar o comando `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=t3st3Un1c0" -e "MSSQL_PID=Express" -p 1439:1433 -d --name=sql mcr.microsoft.com/mssql/server:latest`

Esse comando cria um _container_ Sql Server disponilizando um servidor com usuário `sa` e senha `t3st3Un1c0` rodando na porta `1439`

Caso já possua um servidor Sql Server de qualque versão, altere os os campos _server_, _user_ e _password_ no arquivo `/Projeto/FeiraSP.WEB.API/FeiraSP.WEB.API/appsettings.json` 






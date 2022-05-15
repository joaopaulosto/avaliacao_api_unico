CREATE PROCEDURE SEL_FEIRA (
	@distritoId int,
	@regiao5 nvarchar(10),
	@nome nvarchar(100),
	@bairro nvarchar(50)
)
AS

SELECT [ID]
      ,[LONG]
      ,[LAT]
      ,[SETCENS]
      ,[AREAP]
      ,[CODDIST]
      ,[CODSUBPREF]
      ,[REGIAO5]
      ,[REGIAO8]
      ,[NOME_FEIRA]
      ,[REGISTRO]
      ,[LOGRADOURO]
      ,[NUMERO]
      ,[BAIRRO]
      ,[REFERENCIA]
  FROM [dbo].[TB_FEIRA] WITH(NOLOCK)
  WHERE (CODDIST = @distritoId OR @distritoId IS NULL)
		AND (REGIAO5 LIKE '%' + @regiao5 + '%' OR @regiao5 IS NULL)
		AND (NOME_FEIRA LIKE '%' + @nome + '%' OR @nome IS NULL)
		AND (BAIRRO LIKE '%' + @bairro + '%' OR @bairro IS NULL)
		
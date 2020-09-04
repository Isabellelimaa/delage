IF NOT EXISTS (
		SELECT 1
		FROM endereco
		WHERE logradouro = 'Marechal Floriano Peixoto'
		)
BEGIN
	INSERT INTO endereco (logradouro, numero, complemento, bairro, cidade, uf)
				  VALUES ('Marechal Floriano Peixoto', 550, 'Sala 604', 'Centro', 'Juiz de Fora', 'MG')
END

GO 

IF NOT EXISTS (
		SELECT 1
		FROM entidade
		WHERE fantasia = 'DELAGE CONSULTORIA E SISTEMAS LTDA'
		)
BEGIN
	INSERT INTO entidade (fantasia, idEndereco)
				  VALUES ('DELAGE CONSULTORIA E SISTEMAS LTDA', 1)
END
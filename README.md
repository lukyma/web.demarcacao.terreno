# Introduction 
Aplicação para gestão na demarcação de terrenos. 
Essa aplicação faz o cálculo entre coordenadas geográficas em formato double. 
Cálculos:
Distância entre 2 ou mais pares de coordenadas geográficas.
Cálculo da área entre 3 ou mais pares de coordenadas geográficas.
É possível verificar os retornos ao retornar os terrenos ou através de um cadastro.

# Getting Started
Para acessar a aplicação, favor seguir os passos abaixo:
Url da Documentacao: https://web-api-demarcacao-terreno.herokuapp.com/swagger
Collection Postman: está na raiz do projeto. Está em formato Zip que foi anexado na tarefa do canvas.
Github: https://github.com/lukyma/web.demarcacao.terreno

# Adicional Infos
Usuarios:

Usuário admin -> Acesso Total
Usuario campo -> Acesso Total em Terreno, /api/v1/empreendimento e /api/v1/empreendimento/{id}
Usuario cliente -> /api/v1/empreendimento , /api/v1/empreendimento/{id} , /api/v1/terreno e /api/v1/terreno/{id}
Obs -> A senha de todos é 123 (De qualquer forma estará no swagger)

Regras:
Existe uma regra implementada em um Middleware, onde só é permitido fazer 10 chamadas no intervalo de 1 minuto.


Pode ser que alguns Endpoints de listagem ainda não esteja funcionando :( 


# Contribute
Essa foi uma aplicação construído por Lucas Machado
Foi levado em consideração as boas práticas de mercado.
Conceitos SOLID, Migration, Design Pattern(Strategy) (Library construída por mim https://github.com/lukyma/pattern), 
configurado para rodar no k8s, esteira de CI/CD totalmente funcional no Azure Devops.

Por mais que eu já tinha um solido conhecimento em C#, foi muito importante a construção dessa API, pois aprendi algumas coisas novas. 

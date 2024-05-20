#language: pt-br

Funcionalidade: PedidoTest

A short summary of the feature

@tag1
Cenario: Listar Pedido
	Dado que a url do endpoint 'http://'
	E o método é 'GET'
	Quando chamar o servico
	Entao statuscode da resposta deverá ser 'OK'

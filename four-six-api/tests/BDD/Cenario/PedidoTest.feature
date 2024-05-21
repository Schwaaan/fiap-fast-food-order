#language: pt-br

Funcionalidade: PedidoTest

A short summary of the feature

@tag1
Cenario: Listar Pedido
	Dado a url do endpoint 'http://localhost:5293/'
	Quando chamar um GET para 'pedidos'
	Entao o statuscode code is '200'

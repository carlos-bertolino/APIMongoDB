
# APIMongoDB

Projeto de testes:

 - WebAPI(C#).
 - MongoDB(NoSQL).
 - Documentação API com Swagger.

## Recursos

Manutenção no cadastro de Produtos:
1. Inserir
2. Consultar
3. Editar
4. Excluir
## API Reference

#### Get all items

```http
  GET /api/items
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `api_key` | `string` | **Required**. Your API key |

#### Get item

```http
  GET /api/items/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |

#### add(num1, num2)

Takes two numbers and returns the sum.


## Appendix

Any additional information goes here


## Authors

- [@carlos-bertolino](https://www.github.com/carlos-bertolino)


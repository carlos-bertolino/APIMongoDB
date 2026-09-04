
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

#### Listar todos os produtos

```http
  GET /api/$version/produtos
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `None` | `None` | Lista todos os produtos |

#### Consultar um produto especifico

```http
  GET /api/$version/produtos/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |



## Authors

- [@carlos-bertolino](https://www.github.com/carlos-bertolino)


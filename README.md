O projeto de MVC CatalogosMVC consiste em um sistema de registro de livros onde usuários possuem perfis (contendo Nome e Id) pode registrar livros em formato de cards com imagem (contendo Id próprio, Id do usuário que o adicionou, nome do livro, data da criação do card, status de leitura e imagem anexável pelo usuário). 
	O programa consiste em duas telas principais onde é possível interagir com perfis usuários e seus respectivos registros, além dessas telas principais, o usuário pode ser redirecionado para telas de operações CRUD onde ele pode Criar, Atualizar ou Editar seus perfis existentes e(ou) registros de seus livros.

Regras de negócio:

O Nome do usuário se limita a 50 caracteres.
A data de criação do card é definida no momento de sua criação.
Todos os campos de criação do perfil de usuário e registros devem ser preenchidos.
Para um registro de livro existir, o usuário que fez esse registro deve existir.
Quando um usuário for apagado do sistema, todos os registros relacionados a ele serão apagados antes do perfil ser excluído.
Uma vez que um usuário ou registro for excluído, os mesmos não podem ser recuperados.
Imagens só são aceitas nos formatos png, jpeg e webp.
O Status de leitura dá ao usuário três opções de escolha: "Planejo ler", "Lendo" e "Lido".
No momento da criação do livro é possível definir o seu status de leitura.

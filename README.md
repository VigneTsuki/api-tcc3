API consumida pelo frontend, aplicativo mobile e o ESP32.

Para rodar localmente é necessário a instalação das seguintes tecnologias:<br>
MySql 8.0.34<br>
SDK .NET 7.0

O script do banco de dados se encontra na pasta src/PresencaAutomatizada.Application/Data/Base/Banco.txt

Após a execução do script, configurar no app.settings da aplicação o login e senha do banco de dados:<br>
"SqlDb": "Server=localhost;Port=3306;Database=tccunesc;Uid=root;Pwd=senha"

Para rodar a aplicação é necessário executar o comando "dotnet run" na pasta src/PresencaAutomatizada.Application, na qual se encontra o arquivo .csproj com as configurações do projeto.
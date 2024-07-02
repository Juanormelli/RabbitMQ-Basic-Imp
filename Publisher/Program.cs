using RabbitMQ.Client;
using System.Text;

namespace Publisher
{
  class Program
  {

    static void Main(string[] args)
    {


      //Configurar Conexao 
      var factory = new ConnectionFactory()
      {
        HostName = "localhost"
      };


      // Abrir conexao
      using (var connection = factory.CreateConnection())

      // Criar um Canal onde vamos definir uma mensagem e publicar a mensagem 
      using (var channel = connection.CreateModel())
      {
        // Criação da fila definindo alguns parametros como:

        /*
         Queue:  Nome da fila 
         Durable: Se for igual a true a fila segue ativa apos o reinicio do sever 
         exclusive: Se for true ela so pode ser acessada via a conexao atual e as fila sao excluidas quando a conexao fecha 
         Auto Delete: se igual a true a mensagem sera deletada ´pós os consumidores usarem a fila
         */
        channel.QueueDeclare(queue: "Teste Juan",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        // Criamos a mensagem 
        string Message = "Hello World";


        //Codificamos para bytes 
        var body = Encoding.UTF8.GetBytes(Message);

        //Pucllicamos a mensagem com o nome da dila e a mensagem como corpo da mensagem 
        channel.BasicPublish(exchange: "",
                             routingKey: "Teste Juan",
                             basicProperties: null,
                             body: body);

        Console.WriteLine(Message + " Enviada!");
      }
    }
  }
}
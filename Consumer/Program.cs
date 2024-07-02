using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace Consumer
{
  class Teste
  {
    static void Main(string[] args)
    {

      Console.ReadLine();
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
        // Declaramos a fila de onde vamos consumir nossa mensagens 

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

        // Solicita a entrega das nossas mensagne s

        var consumer = new EventingBasicConsumer(channel);
          Console.WriteLine("Aqui");


        // Após isso vamos receber nossa smensagens do nosso briker
        consumer.Received += async (model, ea) =>
        {
          Console.WriteLine("Aqui");
          //Transformamos a mensgame num array 
          var body = ea.Body.ToArray();

          //Convertemos nossa mensagem para a linguagem ou padrao utf-8
          var message = Encoding.UTF8.GetString(body);

          Console.WriteLine($"Mensagem recebida: {message}");
          Console.ReadLine();
        };

        // Após isso vamos colocar nossa mensagem como consumida no Rabbit MQ

        channel.BasicConsume(queue: "Teste Juan",
                              autoAck: true,
                              consumer: consumer);


        Console.ReadLine();



      }
    }
  }
}
import sys
import pika
import hashlib

credentials = pika.PlainCredentials("root", "itriD200")
connection = pika.BlockingConnection(
    pika.ConnectionParameters(host="localhost", credentials=credentials)
)
channel = connection.channel()
# channel.exchange_declare(exchange="MES.Exchange.LotWip.Update", exchange_type="direct")
# result = channel.queue_declare(exclusive=True)
# queue_name = result.method.queue
queue_name="MES.Queue.LotWip.Update"

channel.queue_bind(exchange="MES.Exchange.LotWip.Update", queue=queue_name)

print(" [*] Waiting for logs. To exit press CTRL+C")


def callback(ch, method, properties, body):
    print(" [x] %r" % (body,))


channel.basic_consume(callback, queue=queue_name, no_ack=True)

channel.start_consuming()

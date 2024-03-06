import sys
import pika
import hashlib

credentials = pika.PlainCredentials("root", "itriD200")
connection = pika.BlockingConnection(
    pika.ConnectionParameters(host="localhost", credentials=credentials)
)
channel = connection.channel()

message = '{"moId":"635752254a4eba6c3665128f","lotId":"635752334a4eba6c36651290","lotOpId":"635752334a4eba6c36651293","resourceType":"E","resourceNo":"CNC02","resourceName":"CNC02","userNo":"itri","userName":"D200","shiftNo":"SF01","shiftName":"日班","dispatchStartDate":"2022-10-26","logDate":"2022-10-28T02:56:36.946Z","logType":"IN","actionNo":null,"logUserId":"60f6604e0c8c2ab36a5c79bd","qty":"20","wipDesc":null,"remark":null}'
channel.basic_publish(
    exchange="MES.Exchange.LotWip.Update",
    routing_key="MES.Queue.LotWip.Update",
    body=message,
)
print(" [x] Sent %r" % (message,))

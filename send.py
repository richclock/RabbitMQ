import pika


credentials = pika.PlainCredentials('root', 'itriD200')
parameters = pika.ConnectionParameters(host='localhost',
                                       credentials=credentials)
connection = pika.BlockingConnection(parameters)
channel = connection.channel()

channel.queue_declare(queue='MES.Queue.LotWip.Update')

msg = '{"moId":"635752254a4eba6c3665128f","lotId":"635752334a4eba6c36651290","lotOpId":"635752334a4eba6c36651293","resourceType":"E","resourceNo":"CNC02","resourceName":"CNC02","userNo":"itri","userName":"D200","shiftNo":"SF01","shiftName":"日班","dispatchStartDate":"2022-10-26","logDate":"2022-10-28T02:56:36.946Z","logType":"IN","actionNo":null,"logUserId":"60f6604e0c8c2ab36a5c79bd","qty":"20","wipDesc":null,"remark":null}'
channel.basic_publish(exchange='MES.Exchange.LotWip.Update', 
                        routing_key='MES.Queue.LotWip.Update', 
                        body=msg)
print(f" [x] Sent '{msg}'")

connection.close()
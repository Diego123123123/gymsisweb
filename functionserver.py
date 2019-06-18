from flask import Flask, request
from flask_restplus import Resource, Api, fields, cors
from flask_mysqldb import MySQL
from flask_cors import CORS
import yaml


app = Flask(__name__)
api = Api(app)
cors = CORS(app, resources={r"/api/*": {"origins": "*"}})

newspaper = api.model('news', {
    'title': fields.String(required=True, description='title'),
    'description': fields.String(required=True, description='user username'),
    'author': fields.String(required=True, description='user password')
})

db = yaml.load(open("functiondb.yml"))
app.config["MYSQL_HOST"] = db["mysql_host"]
app.config["MYSQL_USER"] = db["mysql_user"]
app.config["MYSQL_PASSWORD"] = db["mysql_password"]
app.config["MYSQL_DB"] = db["mysql_db"]

mysql = MySQL(app)

@api.route('/functions')
class Function(Resource):

    def __convertToJson(self, objectList):
        result = []
        print(objectList)
        for value in objectList:
            result.append({
            "functionId": value[0],
            "movieName": value[3],
            "schedule": value[1],
            "room": value[2]
            })
        return result

    def get(self):
        cur = mysql.connection.cursor()
        query = "SELECT * FROM functions"
        newspapers = cur.execute(query)
        newspapersDetails = cur.fetchall()
        result = self.__convertToJson(newspapersDetails)
        return result , 200 , {'Access-Control-Allow-Origin': '*'}

    def post(self):
        data = request.json
        cur = mysql.connection.cursor()
        query = "INSERT INTO functions(movieName, schedule, room) values ('%s', '%s', '%s');" % (data['movieName'], data['schedule'], data['room'])
        cur.execute(query)
        print(query)
        mysql.connection.commit()
        cur.close()
        return "success", 200,{'Access-Control-Allow-Origin': '*'}

@api.route('/functions/<int:id>')
class FunctionsById(Resource):

    def __parseToJSON(self, objectToParse):
        return {
                "functionId": objectToParse[0],
                "movieName": objectToParse[3],
                "schedule": objectToParse[1],
                "room": objectToParse[2]
                }

    def delete(self, id):
        cur = mysql.connection.cursor()
        query = "DELETE FROM functions WHERE functionId=%s" % (id)
        cur.execute(query)
        print(query)
        mysql.connection.commit()
        cur.close()
        return "success", 200


    def put(self, id):
        data = request.json
        cur = mysql.connection.cursor()
        query = "UPDATE functions set movieName='%s' , schedule='%s' , room='%s' where functionId=%s" % (data['movieName'], data['schedule'], data['room'], id)
        cur.execute(query)
        print(query)
        mysql.connection.commit()
        cur.close()
        return "success", 200

    def get(self, id):
        cur = mysql.connection.cursor()
        query = "SELECT * FROM functions WHERE functionId=%s" % id
        newspapers = cur.execute(query)
        newspapersDetails = cur.fetchone()
        return self.__parseToJSON(newspapersDetails) , 200 , {'Access-Control-Allow-Origin': '*'}

if __name__ == "__main__":
    app.run(debug=True)

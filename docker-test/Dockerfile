#define what image we want to build from
FROM node:14

#create app directory
WORKDIR /usr/scr/app

RUN npm install

COPY package*.json ./

#To bundle app's source code inside the Docker image
#COPY . .

EXPOSE 8080

CMD ["npm", "start"]



#!bin/bash

minikube start --alsologtostderr --v=1
minikube addons enable ingress
eval $(minikube docker-env)
kubectl apply -f kafka_setup/
docker build -t service-one services_cs_projects/Service1/.
docker build -t service-two services_cs_projects/Service2/.

kubectl apply -f service_and_deployment


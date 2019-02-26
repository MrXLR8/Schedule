Для работы необходим .NETCore

1) Выполнить следующие комманды в терминале:

sudo sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893
sudo apt-get update
sudo apt-get install dotnet-dev-1.0.4

2) Для запуска сервера открываем терминал в директории и пишем следующее:

dotnet Server.dll

3) Готово, сервер запущен. Для справки набирайте help.

Дополнительная информации об установке на сайте Microsoft:
https://www.microsoft.com/net/core#linuxubuntu
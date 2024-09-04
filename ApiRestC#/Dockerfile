#FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
#WORKDIR webapp
#
##Expongo los puertos necesarios
#EXPOSE 80
#EXPOSE 5123
#EXPOSE 7109
#
##Copio el proyecto principal y lo restauro
#COPY ./*.csproj ./
#RUN dotnet restore
#
##Ejecuto el programa principal generando los archivos totalmente necesarios
#COPY . .
#RUN dotnet publish -c Release -o out
#
##Construyo la imagen a partir de la dll
#From mcr.microsoft.com/dotnet/sdk:8.0
#WORKDIR /webapp
#Copy --from=build /webapp/out .
#ENTRYPOINT ["dotnet", "ProyectoJWT.dll"]




#------------------------------
#Comandos usados para levantar el docker

#Esto compila el codigo en una imagen
# docker build -t proyectojwt .
# docker build -t NombreImagen .

# docker images

#Esto me permite ejecutar una imagen y crear el docker
# docker run -d -p 5123:5123 -p 5123:5123 --name webapp proyectojwt
# docker run -d -p 5024:5024 --name webapp nombre

#Parar el docker
# docker stop webapp

#Eliminar el docker
# docker rm webapp

#Exportar una img 
# docker save -o ./Exportar.tar proyectojwt

#Importar una img
# docker load -i ./Exportar.tar
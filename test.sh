dotnet dev-certs https --clean

if [ -d ~/.microsoft ]; then
    rm -r ~/.microsoft
fi

if [ -d ~/.aspnet ]; then
    rm -r ~/.aspnet
fi

dotnet dev-certs https --trust
dotnet dev-certs https -ep ~/.aspnet/https/AdSpot.Api.pfx -p helloworld

mkdir -p ~/.microsoft/usersecrets/49f60c6d-9fd8-435a-8089-f22f3e378282
echo '{\n    "Kestrel:Certificates:Development:Password": "helloworld"\n}' > ~/.microsoft/usersecrets/49f60c6d-9fd8-435a-8089-f22f3e378282/secrets.json

ln -s ~/.aspnet ~/ASP.NET
ln -s ~/.aspnet/https ~/ASP.NET/https
ln -s ~/.microsoft ~/Microsoft
ln -s ~/.microsoft/usersecrets ~/Microsoft/usersecrets

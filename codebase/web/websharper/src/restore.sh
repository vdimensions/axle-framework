paket='.paket/paket.sh'
project='Axle.Web.WebSharper'

rm -rf paket-files/ && $paket update && rm -rf obj/ && dotnet restore $project.csproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
$paket simplify
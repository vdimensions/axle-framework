msbuild="../../../../submodules/btw/msbuild.sh"
paket='.paket/paket.sh'
project='Axle.Data.SqlClient.Tests'

$paket update
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi

rm -rf obj/
dotnet restore $project.csproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
$msbuild $project.csproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
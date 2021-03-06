msbuild="../../../../submodules/btw/msbuild.sh"
project='Axle.Data.FSharp'

 ./restore.sh

dotnet clean $project.fsproj && $msbuild $project.fsproj && dotnet pack $project.fsproj --no-build
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
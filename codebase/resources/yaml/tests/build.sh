msbuild="../../../../submodules/btw/msbuild.sh"
project='Axle.Resources.Yaml.Tests'

./restore.sh

$msbuild $project.csproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
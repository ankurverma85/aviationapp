cmake_minimum_required(VERSION 3.14)
project(AviationApp VERSION 0.0.0)
include(InstallRequiredSystemLibraries)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)

set(CMAKE_CXX_STANDARD 17)

find_package(Qt5 REQUIRED COMPONENTS Qml Quick Location)
find_package(Qt5QuickCompiler REQUIRED)

qtquick_compiler_add_resources(MAIN_RESOURCES qml.qrc)
add_executable(main main.cpp ${MAIN_RESOURCES})
qt5_use_modules(main Qml Quick Location)
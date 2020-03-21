cmake_minimum_required(VERSION 3.14)
project(AviationApp VERSION 0.0.0)
include(InstallRequiredSystemLibraries)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)

set(CMAKE_CXX_STANDARD 17)

find_package(Qt5 CONFIG REQUIRED Core Qml Quick QuickCompiler)

qtquick_compiler_add_resources(MAIN_RESOURCES qml.qrc)
add_executable(main main.cpp squircle.cpp ${MAIN_RESOURCES})
target_link_libraries(
	main PRIVATE
	Qt5::Core Qt5::Gui Qt5::Qml Qt5::Quick
	)
add_custom_command(
	TARGET main
	POST_BUILD
	COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:Qt5::Core> $<TARGET_FILE_DIR:main>
	COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:Qt5::Gui> $<TARGET_FILE_DIR:main>
	COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:Qt5::Qml> $<TARGET_FILE_DIR:main>
	COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:Qt5::Quick> $<TARGET_FILE_DIR:main>
)
include(buildenv.cmake)

function(myexec workdir)
	execute_process(
		COMMAND ${ARGN}
		WORKING_DIRECTORY ${workdir}
		RESULT_VARIABLE result
		ERROR_VARIABLE error
		OUTPUT_VARIABLE output)
	if(NOT "${result}" STREQUAL "0")
		message(FATAL_ERROR 
		"Error Executing Command : ${ARGN}\n"
		"WorkDir: ${workdir}\n" 
		"Result: ${result} \n"
		"Error: ${error} \n"
		"Output: ${output}")
	endif()
endfunction()

function(init_vcpkg)
	if (VCPKG_ROOT AND VCPKG_EXEC)
		return()
	endif()

	DetectPath(DEVEL_BUILDPATH ${CMAKE_BINARY_DIR})
	DetectPath(BUILD_TOOLS_DIR ${DEVEL_BUILDPATH}/tools)
	DetectPath(VCPKG_ROOT ${DEVEL_BUILDPATH}/vcpkg)

	file(LOCK ${DEVEL_BUILDPATH} DIRECTORY GUARD FUNCTION)
	set(CMAKE_TOOLCHAIN_FILE ${VCPKG_ROOT}/scripts/buildsystems/vcpkg.cmake CACHE PATH "")
	if(NOT EXISTS ${VCPKG_ROOT}/README.md)
		message("Cloning vcpkg in ${VCPKG_ROOT}")
		execute_process(COMMAND git clone https://github.com/Microsoft/vcpkg.git ${VCPKG_ROOT})
# Open ssl issue
		#execute_process(COMMAND git -C ${VCPKG_ROOT} checkout bdae0904c41a0ee2c5204d6449038d3b5d551726~1)
	endif()
	if(NOT EXISTS ${VCPKG_ROOT}/README.md)
		message(FATAL_ERROR "***** FATAL ERROR: Could not clone vcpkg *****")
	endif()

	foreach (sys linux windows uwp)
		if (sys MATCHES "linux")
			set (sys-contents "set(VCPKG_CMAKE_SYSTEM_NAME Linux)")
		elseif (sys MATCHES "uwp")
			set (sys-contents "set(VCPKG_CMAKE_SYSTEM_NAME WindowsStore)\nset(VCPKG_CMAKE_SYSTEM_VERSION 10.0)")
		else()
			set(sys-contents "")
		endif()
		foreach(arch x86 x64 arm arm64)
			set(arch-contents "set(VCPKG_TARGET_ARCHITECTURE ${arch})\nset(VCPKG_CRT_LINKAGE dynamic)\nset(VCPKG_LIBRARY_LINKAGE static)")
			file(WRITE ${VCPKG_ROOT}/triplets/${arch}-${sys}.cmake "${arch-contents}\n${sys-contents}")
		endforeach()
	endforeach()
	
	find_program(VCPKG_EXEC vcpkg PATHS ${VCPKG_ROOT})

	if (NOT EXISTS ${VCPKG_EXEC})
		if (WIN32)
			find_program(VCPKG_BOOTSTRAP NAMES bootstrap-vcpkg.bat PATHS ${VCPKG_ROOT})
		else()
			find_program(VCPKG_BOOTSTRAP NAMES bootstrap-vcpkg.sh PATHS ${VCPKG_ROOT})
		endif()
		myexec(${VCPKG_ROOT} ${VCPKG_BOOTSTRAP})
	endif()

	find_program(VCPKG_EXEC vcpkg PATHS ${VCPKG_ROOT})
	if(NOT EXISTS ${VCPKG_EXEC})
		message(FATAL_ERROR "***** FATAL ERROR: Could not bootstrap vcpkg ***** : ${VCPKG_EXEC}")
	endif()
	if(NOT EXISTS ${CMAKE_TOOLCHAIN_FILE})
		message(FATAL_ERROR "***** FATAL ERROR: Could not bootstrap vcpkg ***** : ${CMAKE_TOOLCHAIN_FILE}")
	endif()
endfunction()

if(CMAKE_SYSTEM_NAME STREQUAL "Linux" OR (NOT CMAKE_SYSTEM_NAME AND CMAKE_HOST_SYSTEM_NAME STREQUAL "Linux"))
	if (NOT BUILD_ARCH)
		message(FATAL_ERROR "Linux builds requrie a BUILD_ARCH")
	endif()
	set (VCPKG_TARGET_TRIPLET ${BUILD_ARCH}-linux CACHE STRING "" FORCE)
endif()

init_vcpkg()

file(LOCK ${DEVEL_BUILDPATH} DIRECTORY)
include(${VCPKG_ROOT}/scripts/buildsystems/vcpkg.cmake)
file(LOCK ${DEVEL_BUILDPATH} DIRECTORY RELEASE)

function(vcpkg_download library)
	file(LOCK ${DEVEL_BUILDPATH} DIRECTORY GUARD FUNCTION)
	execute_process(COMMAND ${VCPKG_EXEC} install ${library}:${VCPKG_TARGET_TRIPLET} WORKING_DIRECTORY ${VCPKG_ROOT} OUTPUT_VARIABLE err RESULT_VARIABLE rslt COMMAND_ECHO STDOUT)
	if (NOT ${rslt} STREQUAL "0")
		message(FATAL_ERROR "${rslt} ${err}")
	endif()
endfunction()

function(use_qt5 target)
	vcpkg_download(qt5)
	find_package(Qt5OpenGL REQUIRED)
	target_link_libraries(${target} PRIVATE Qt5OpenGL)
endfunction()


function(use_nanogui target)
	find_package(OpenGL REQUIRED)
	find_package(GLUT REQUIRED)
	find_package(glfw3 REQUIRED)
	vcpkg_download(glad)
	vcpkg_download(nanogui)
	find_package(nanovg)
	find_path(NANOGUI_INCLUDE include/nanogui)
	find_library(NANOGUI_LIB nanogui)
	target_compile_definitions(${target} PRIVATE GLAD_GLAPI_EXPORT)
	target_include_directories(${target} PRIVATE ${NANOGUI_INCLUDE}/include ${GLUT_INCLUDE_DIRS} ${OPENGL_INCLUDE_DIRS})
	target_link_libraries(${target} PRIVATE ${NANOGUI_LIB}  ${GLUT_LIBRARY} opengl32 glfw3)
endfunction()

function(use_angle target)
	vcpkg_download(angle)
	find_package(unofficial-angle REQUIRED)
	target_link_libraries(${target} PRIVATE unofficial::angle::libEGL unofficial::angle::libGLESv2)
endfunction()

project(AviationApp)
include(vcpkg.cmake)
find_package(OpenGL REQUIRED)
set (SrcRoot ${CMAKE_CURRENT_LIST_DIR}/../src)
# ------------------------------------------------------------------------------
# Qt path settings
# ------------------------------------------------------------------------------
#set(QT5_ROOT ${QT5_ROOT} CACHE PATH "QT5-NOT_FOUND")
#if (NOT EXISTS "${QT5_ROOT}/lib/cmake")
#  message(FATAL_ERROR "[ERROR] Qt libraries not found!!")
#else()
#  message(STATUS "Qt5: ${QT5_ROOT}")
#  set(CMAKE_PREFIX_PATH ${CMAKE_PREFIX_PATH} ${QT5_ROOT})
#endif()


# ------------------------------------------------------------------------------
# Find packages
# ------------------------------------------------------------------------------
#find_package(Qt5Widgets REQUIRED)
#find_package(Qt5OpenGL REQUIRED)

add_executable(AviationApp ${SrcRoot}/main.cpp)
use_nanogui(AviationApp)

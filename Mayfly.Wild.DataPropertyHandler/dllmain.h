// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>
#include <new>  // std::nothrow

class __declspec(uuid("EE8CE4D1-9555-4469-8F10-753C73F5169D")) CWildDataPropertyHandler;
HRESULT CWildDataPropertyHandler_CreateInstance(REFIID riid, void **ppv);
HRESULT RegisterHandler();
HRESULT UnregisterHandler();

void DllAddRef();
void DllRelease();



// TODO: reference additional headers your program requires here

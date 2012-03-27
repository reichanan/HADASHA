#pragma once

#pragma managed




#using <mscorlib.dll>
#include <comutil.h>
#include <windows.h>
#include <atlstr.h>
#include "olemission.tlh"
#include <string>

#include <iostream>
#include <fstream>
using namespace std;


using namespace System;

using namespace System::IO;

using namespace System::Collections;

using namespace olemission ;

olemission::IclsmissionPtr ptrclsmission = NULL;

void to_variant(const std::string& str, VARIANT& vt)
{
	_bstr_t bs(str.c_str());
	reinterpret_cast<_variant_t&>(vt) = bs;
}

__gc public class OperateTower
{
	public:
		static OperateTower *s_instance = 0;

		static OperateTower* instance()
		{
			if (!s_instance)
			{
			  s_instance = new OperateTower;
			  Init();
			}
			return s_instance;
		}

    static void ReleaseNew()
	{
		if(s_instance)
		{
			ptrclsmission->Release();
			ptrclsmission.Detach();
			s_instance = NULL;
			ptrclsmission = NULL;
		}
		
		
	}

	static int Init()
	{
		
		
		HRESULT hr = S_OK;

		hr = ptrclsmission.CreateInstance(__uuidof(clsmission));

		if(FAILED(hr))
		{
			printf("Error instantiating Connection object\n");

		}

		_variant_t v1 = "c:\\win_ese5\\initolemission.ini";  
		_variant_t ret1 = ptrclsmission->initialize(&v1);

		return ret1;


	}



		
		int Delall(int machine);
		int Del(int LU);
		void Read(int LU);
		void Release();
		int LUCall(int LU, int machine, int exitcode);
		int RemotePickingEnd(int LU, int machine, int exitcode);
		int ClearTable();
		int DBMaint();

		void SetTimeOut(int machine);

private:
		OperateTower(){m_timeout = 0; };
		//~OperateTower(){;};
		int m_timeout;

		
};
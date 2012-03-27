
#include "operatetowerdll.h"


int OperateTower::LUCall(int LU, int machine, int exitcode)
{
	_variant_t v1 = LU;  
	_variant_t v2 = "C";  
	_variant_t v3 = machine;  
	_variant_t v4 = "B";  
	_variant_t v5 = exitcode;  
	_variant_t ret2 = ptrclsmission->create(&v1, &v2, &v3, &v4, &v5);
	Sleep(m_timeout);

	return ret2;


}


int OperateTower::RemotePickingEnd(int LU, int machine, int exitcode)
{
	_variant_t v1 = LU;  
	_variant_t v2 = "B";  
	_variant_t v3 = machine;  
	_variant_t v4 = "C";  
	_variant_t v5 = exitcode;  
	_variant_t ret2 = ptrclsmission->create(&v1, &v2, &v3, &v4, &v5);
	Sleep(m_timeout);

	return ret2;

}




void OperateTower::Release()
{


	ptrclsmission->Release();
	ptrclsmission = NULL;
	//Sleep(5000);

}


int OperateTower::Delall(int machine)
{

	_variant_t v1 = machine;
	_variant_t ret1 = ptrclsmission->delall(&v1);
	Sleep(m_timeout);
	return ret1;


}



int OperateTower::Del(int LU)
{
	_variant_t v1 = LU;
	_variant_t ret1 = ptrclsmission->del(&v1);
	Sleep(m_timeout);
	return ret1;

}


void OperateTower::Read(int LU)
{
	ofstream myfile;
	myfile.open ("lu.txt");
	_variant_t v1 = LU;
	_variant_t ret1 = ptrclsmission->read(&v1);
	_bstr_t bs2 = ret1;  
	const std::string st = bs2;
	myfile << st ;
	myfile.close();

}



int OperateTower::ClearTable()
{
	_variant_t ret1 = ptrclsmission->zaptab();
	Sleep(m_timeout);
	return ret1;
	

}




int OperateTower::DBMaint()
{
	_variant_t ret1 = ptrclsmission->manuttab();
	return ret1;

}


void  OperateTower::SetTimeOut(int machine)
{

	m_timeout = machine;
}
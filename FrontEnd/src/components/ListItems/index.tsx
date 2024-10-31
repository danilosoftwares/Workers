import React, { useState, useEffect, useCallback, useRef } from 'react';
import { FilterMatchMode, FilterOperator } from 'primereact/api';
import { DataTable, DataTableFilterMeta, DataTableFilterMetaData } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { InputText } from 'primereact/inputtext';
import { IconField } from 'primereact/iconfield';
import { InputIcon } from 'primereact/inputicon';
import { Button } from 'primereact/button';
import { GetAllWorkers, RemoveWorker } from '../../api/service';
import { BoxButtonsAction, BoxHeader, BoxHeaderOut, Container } from './styles';
import { WorkersAllReponse } from '../../api/types';
import { ListItemsProps } from './types';
import { useNavigate } from 'react-router-dom';
import { AxiosError } from 'axios';
import { Toast } from 'primereact/toast';
import { Tag } from 'primereact/tag';


const defaultFilters: DataTableFilterMeta = {
  global: { value: null, matchMode: FilterMatchMode.CONTAINS },
  firstName: {
    operator: FilterOperator.AND,
    constraints: [{ value: null, matchMode: FilterMatchMode.STARTS_WITH }],
  },
  lastName: {
    operator: FilterOperator.AND,
    constraints: [{ value: null, matchMode: FilterMatchMode.STARTS_WITH }],
  },
  corporateEmail: {
    operator: FilterOperator.AND,
    constraints: [{ value: null, matchMode: FilterMatchMode.STARTS_WITH }],
  },
  workerNumber: {
    operator: FilterOperator.AND,
    constraints: [{ value: null, matchMode: FilterMatchMode.EQUALS }],
  },
};

export const ListItems: React.FC<ListItemsProps> = ({}) => {
  const navigate = useNavigate();
  const [customers, setCustomers] = useState<WorkersAllReponse[]>([]);
  const [filters, setFilters] = useState<DataTableFilterMeta>(defaultFilters);
  const [loading, setLoading] = useState<boolean>(false);
  const [globalFilterValue, setGlobalFilterValue] = useState<string>('');

  const toast = useRef<Toast>(null);

  const clearFilter = () => {
    initFilters();
  };

  const onGlobalFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    let _filters = { ...filters };
    (_filters['global'] as DataTableFilterMetaData).value = value;
    setFilters(_filters);
    setGlobalFilterValue(value);
  };

  const initFilters = () => {
    let _filters = { ...filters };
    (_filters['global'] as DataTableFilterMetaData).value = '';
    setFilters(_filters);
    setGlobalFilterValue('');
  };

  const createWorker = () => {
    navigate(`/edit/-1`);
  }

  const exitSystem = () => {
    localStorage.removeItem('auth');
    window.location.reload();
  }

  const removeWorker = async (id:number) => {
    const result = await RemoveWorker(id);
    if (result.status){
      toast.current?.show({ severity: 'success', summary: 'Success', detail: 'Funcionario removido com sucesso!', life: 1000 });
      fechLoadingList();
    } else {
      toast.current?.show({ severity: 'error', summary: 'Erro', detail: result.message, life: 3000 });
    }
  }

  const priceBodyTemplate = (rowData: WorkersAllReponse) => {
    return (
      <BoxButtonsAction>
        <Button label="Editar" size="small" onClick={()=>navigate(`/edit/${rowData.id}`)} />
        <Button label="Excluir" size="small" severity="danger" onClick={()=>removeWorker(rowData.id)} />
      </BoxButtonsAction>);
  };

  const fechLoadingList = useCallback(async () => {
    try{
      setLoading(true);
      const result = await GetAllWorkers();
      setCustomers(result);
      setLoading(false);
    } catch(e: any) {
      const error = e as AxiosError;
      if (error.status===401){
        localStorage.removeItem('auth');
        window.location.reload();
      }
    }
  }, [GetAllWorkers]);

  useEffect(() => {
    fechLoadingList();
    initFilters();
  }, []);

  const renderHeader = () => {
    return (
      <BoxHeader>
        <Button type="button" icon="pi pi-filter-slash" label="Clear" outlined onClick={clearFilter} />
        <IconField iconPosition="left">
          <InputIcon className="pi pi-search" />
          <InputText value={globalFilterValue} onChange={onGlobalFilterChange} placeholder="Keyword Search" />
        </IconField>
        <Button icon="pi pi-plus" rounded raised onClick={()=>createWorker()} />
      </BoxHeader>
    );
  };

  const header = renderHeader();

  const headerUser = () => {
    const user = localStorage.getItem(`user`);
    if (user){
      const email = JSON.parse(user).email;
      return email;
    }
  }

  return (
    <Container>
      <Toast ref={toast} />
      <BoxHeaderOut>
      <Button label="Sair do Sistema" size="small" onClick={exitSystem} />
      <Tag severity="success" value={headerUser()} rounded></Tag>
      </BoxHeaderOut>
      <DataTable value={customers} style={{ width: "100%" }} scrollHeight="400px" paginator showGridlines rows={10} loading={loading} dataKey="id"
        filters={filters} globalFilterFields={['firstName', 'lastName', 'corporateEmail', 'workerNumber']} header={header}
        emptyMessage="No customers found." onFilter={(e) => setFilters(e.filters)}>
        <Column body={priceBodyTemplate} header="Ação" />
        <Column field="firstName" header="First Name" filter filterPlaceholder="Search by first name" style={{ minWidth: '12rem' }} />
        <Column field="lastName" header="Last Name" filter filterPlaceholder="Search by last name" style={{ minWidth: '12rem' }} />
        <Column field="corporateEmail" header="Corporate Email" filter filterPlaceholder="Search by email" style={{ minWidth: '16rem' }} />
        <Column field="workerNumber" header="Worker Number" filter filterPlaceholder="Search by worker number" style={{ minWidth: '12rem' }} />
      </DataTable>
    </Container>
  );
}

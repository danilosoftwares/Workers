import React, { useCallback, useEffect, useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { Password } from 'primereact/password';
import { Calendar } from 'primereact/calendar';
import { InputMask } from 'primereact/inputmask';
import { Toast } from 'primereact/toast';
import { Divider } from 'primereact/divider';
import { CreateWorker, EditWorker, GetAllWorkers, GetWorker } from '../../api/service';
import { useNavigate, useParams } from 'react-router-dom';
import { WorkersAllReponse, WorkersItem } from '../../api/types';
import { AxiosError } from 'axios';
import { BoxButtons, BoxLoading, BoxMain, BoxPhones } from './styles';
import { Dropdown, DropdownChangeEvent } from 'primereact/dropdown';
import { ListBox, ListBoxChangeEvent } from 'primereact/listbox';


interface WorkerLeader {
  name: string;
  code: string;
}

interface WorkerPhones {
  phone: string;
  code: string;
}

export const EditItem: React.FC = () => {
  const params = useParams();
  const navigate = useNavigate();

  const [phone, setPhone] = useState('');
  const [selectedPhones, setSelectedPhones] = useState<string | null>(null);
  const [listPhones, setListPhones] = useState<WorkerPhones[]>([]);
  const [workersLeaders, setWorkersLeaders] = useState<WorkerLeader[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [submitted, setSubmitted] = useState<boolean>(false);
  const [worker, setWorker] = useState<WorkersItem>({
    id: 0,
    firstName: '',
    lastName: '',
    corporateEmail: '',
    workerNumber: '',
    phones: [],
    leaderName: null,
    passwordHash: '',
  });

  const toast = React.useRef<Toast>(null);

  const handleDropDownChange = (e: DropdownChangeEvent) => {
    const { name, value } = e.target;
    setWorker((prevCustomer) => ({ ...prevCustomer, [name]: value }));
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setWorker((prevCustomer) => ({ ...prevCustomer, [name]: value }));
  };

  const errorField = (field: string) => {
    return submitted && !field && <small className="p-error">Campo obrigatório.</small>
  }

  const isEdit = () => {
    try{
      const id = params["id"];
      return id !== "-1";
    } catch(e){
      return false;
    }
  }

  const handleReturn = () => {
    navigate(`/`)
  }

  const removePhone = () => {
    if (selectedPhones) {
      const newlist = listPhones.filter(f=>selectedPhones?f.phone!==selectedPhones:true);
      setListPhones(newlist);
    }
  }

  const handleSubmit = async () => {
    try {
      setSubmitted(false);
      if (worker.firstName && worker.lastName && worker.passwordHash  && worker.corporateEmail && worker.workerNumber) {
        worker.phones = listPhones.map(m=>m.phone);
        const result = !isEdit() ? await CreateWorker(worker) : await EditWorker(worker);
        if (result.status) {
          toast.current?.show({ severity: 'success', summary: 'Success', detail: 'Funcionario alterado com sucesso!', life: 1000 });
          setTimeout(() => {
            navigate(`/`);
          }, 1200);
        } else {
          toast.current?.show({ severity: 'error', summary: 'Erro', detail: result.message, life: 3000 });
        }
      }
      setSubmitted(true);
    } catch (e: any) {
      const error = e as AxiosError;
      if (error.status === 401) {
        localStorage.removeItem('auth');
        window.location.reload();
      }
    }
  };


  const fechLoadingItem = useCallback(async () => {
    try {
      setLoading(true);
      const allWorkers = await GetAllWorkers();
      const listSelect = allWorkers.map<WorkerLeader>((item: WorkersAllReponse) => {
        return {
          name: item.firstName + ' ' + item.lastName + ' ' + item.workerNumber,
          code: item.id.toString(),
        }
      })
      setWorkersLeaders(listSelect);

      if (isEdit()) {
        const id = params["id"];
        const item = await GetWorker(id ? id : ``);
        const changed = ({ ...item } as WorkersItem);
        setWorker(changed);

        if (changed.phones) {
          const allPhones = changed.phones?.map(item => {
            return {
              phone: item,
              code: item,
            }
          });
          setListPhones(allPhones);
        }
      }

      setLoading(false);
    } catch (e: any) {
      const error = e as AxiosError;
      if (error.status === 401) {
        localStorage.removeItem('auth');
        window.location.reload();
      }
    }

  }, [GetWorker]);

  useEffect(() => {
    fechLoadingItem();
  }, []);

  return (
    <div className="p-fluid register-form" style={{ maxWidth: '600px', margin: 'auto' }}>
      <Toast ref={toast} />
      <h2>Registrar Funcionário</h2>
      {loading ?
        <BoxLoading>
          <i className="pi pi-spin pi-spinner" style={{ fontSize: '2rem' }}></i>
        </BoxLoading> :
        (
          <BoxMain>
            <div className="p-grid p-formgrid">
              <div className="p-col-12 p-md-6">
                <label htmlFor="firstname">Nome</label>
                <InputText id="firstname" name="firstName" value={worker.firstName} onChange={handleInputChange} />
                {errorField(worker.firstName)}
              </div>
              <div className="p-col-12 p-md-6">
                <label htmlFor="lastname">Sobrenome</label>
                <InputText id="lastname" name="lastName" value={worker.lastName} onChange={handleInputChange} />
                {errorField(worker.lastName)}
              </div>
              <div className="p-col-12 p-md-6">
                <label htmlFor="corporateemail">Email Corporativo</label>
                <InputText id="corporateemail" name="corporateEmail" keyfilter="email" type="email" value={worker.corporateEmail} onChange={handleInputChange} />
                {errorField(worker.corporateEmail)}
              </div>
              <div className="p-col-12 p-md-6">
                <label htmlFor="workernumber">Chapa</label>
                <InputText id="workernumber" name="workerNumber" value={worker.workerNumber} onChange={handleInputChange} />
                {errorField(worker.workerNumber)}
              </div>
              <div className="p-col-12 p-md-6">
                <label htmlFor="leaderName">Lider</label>
                <div className="card flex justify-content-center">
                  <Dropdown
                    value={worker.leaderName?.toString()}
                    onChange={handleDropDownChange}
                    options={workersLeaders}
                    optionLabel="name"
                    optionValue="code"
                    name="leaderName"
                    placeholder="Selecione o Lider"
                    filter
                    className="w-full md:w-14rem" />
                </div>
              </div>
              <div className="p-col-12 p-md-6">
                <label htmlFor="passwordhash">Senha</label>
                <Password id="passwordHash" name="passwordHash" value={worker.passwordHash} onChange={handleInputChange} toggleMask />
                {errorField(worker.lastName)}
              </div>
            </div>
            <BoxPhones>
              <div className="p-col-12 p-md-6">
              <label htmlFor="phone" className="font-bold block mb-2">Telefone</label>
              <InputMask id="phone" name="phone" value={phone} onChange={(e)=>e.target.value?setPhone(e.target.value):null} 
              mask="(99) 9.9999-9999" 
              placeholder="(11) 9.9999-9999">

              </InputMask>

                </div>
              <Button label="Adicionar" disabled={!phone} size="small" onClick={()=>setListPhones([...listPhones, {phone:phone,code:phone}])} />
              <ListBox
                value={selectedPhones}
                onChange={(e: ListBoxChangeEvent) => setSelectedPhones(e.value)}
                options={listPhones}
                optionLabel="phone"
                optionValue="code"
                className="w-full md:w-14rem"
              />
               <Button disabled={!selectedPhones} label="Remover" severity='danger' size="small" onClick={()=>removePhone()} />
             
            </BoxPhones>
          </BoxMain>
        )}

      <Divider />
      <BoxButtons>
        <Button label="Voltar" icon="pi pi-arrow-left" severity="secondary" onClick={handleReturn} className="p-mt-3" />
        <Button label="Registrar" icon="pi pi-check" onClick={handleSubmit} className="p-mt-3" />
      </BoxButtons>
    </div>
  );
};

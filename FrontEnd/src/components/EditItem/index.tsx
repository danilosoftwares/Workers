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
import { BoxButtons, BoxLoading } from './styles';
import { Dropdown, DropdownChangeEvent } from 'primereact/dropdown';


interface WorkerLeader {
  name: string;
  code: string;
}

export const EditItem: React.FC = () => {
  const params = useParams();
  const navigate = useNavigate();

  const [workersLeaders, setWorkersLeaders] = useState<WorkerLeader[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [submitted, setSubmitted] = useState<boolean>(false);
  const [worker, setWorker] = useState<WorkersItem>({
    id: 0,
    firstName: '',
    lastName: '',
    corporateEmail: '',
    workerNumber: '',
    phonenumbers: '',
    leaderName: '',
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

  const handleReturn = () => {
    navigate(`/`)
  }

  const handleSubmit = async () => {
    setSubmitted(false);
    const id = params["id"];
    const result = id === "-1" ? await CreateWorker(worker) : await EditWorker(worker);
    if (result.status) {
      toast.current?.show({ severity: 'success', summary: 'Success', detail: 'Funcionario alterado com sucesso!', life: 1000 });
      setTimeout(() => {
        navigate(`/`);
      }, 1200);
    } else {
      toast.current?.show({ severity: 'error', summary: 'Erro', detail: result.message, life: 3000 });
    }
    setSubmitted(true);
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

      const id = params["id"];
      if (id !== "-1") {
        const item = await GetWorker(id ? id : ``);
        const changed = ({ ...item } as WorkersItem);
        setWorker(changed);
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
              <label htmlFor="phonenumbers">Phone Number</label>
              <InputMask id="phonenumbers" name="phonenumbers" mask="+1-999-999-9999" value={worker.phonenumbers} />
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
            </div>


          </div>)}

      <Divider />
      <BoxButtons>
        <Button label="Voltar" icon="pi pi-arrow-left" severity="secondary" onClick={handleReturn} className="p-mt-3" />
        <Button label="Registrar" icon="pi pi-check" onClick={handleSubmit} className="p-mt-3" />
      </BoxButtons>
    </div>
  );
};

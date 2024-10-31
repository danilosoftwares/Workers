import React, { useState, useRef, FormEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import { Password } from 'primereact/password';
import { Button } from 'primereact/button';
import { Toast } from 'primereact/toast';
import { classNames } from 'primereact/utils';
import { BoxToLogin, FormCustom, InputBox, VoltarLabel } from './styles';
import { RegisterProps } from './types';
import { PostLogin, PostRegister } from '../../api/service';

export const Register: React.FC<RegisterProps> = ({onLogin}) => {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [submitted, setSubmitted] = useState<boolean>(false);
    const toast = useRef<Toast>(null);

    const handleLogin = async (e: FormEvent) => {
        e.preventDefault();
        setSubmitted(true);

        const result = await PostRegister(email,password);
        if (result.status) {
            const valid = await PostLogin(email,password);
            if (valid.status) {
                localStorage.setItem('auth',JSON.stringify(valid.token));
                localStorage.setItem('user',JSON.stringify(valid.user));
                window.location.reload();
            } else {
                toast.current?.show({ severity: 'error', summary: 'Erro', detail: valid.message, life: 3000 });
            }
        } else {
            toast.current?.show({ severity: 'error', summary: 'Erro', detail: result.message, life: 3000 });
        }
    };

    const getEmailClass = () => {
        return classNames({ 'p-invalid': submitted && !email });
    };

    const getPasswordClass = () => {
        return classNames({ 'p-invalid': submitted && !password });
    };

    return (
        <>
            <Toast ref={toast} />
            <FormCustom onSubmit={handleLogin}>
                <h2>Registro</h2>
                <InputBox>
                    <label htmlFor="email">Email</label>
                    <InputText
                        id="email"
                        value={email}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEmail(e.target.value)}
                        className={getEmailClass()}
                    />
                    {submitted && !email && <small className="p-error">Email é obrigatório.</small>}
                </InputBox>
                
                <InputBox>
                    <label htmlFor="password">Senha</label>
                    <Password
                        id="password"
                        value={password}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPassword(e.target.value)}
                        className={getPasswordClass()}
                        feedback={false}
                    />
                    {submitted && !password && <small className="p-error">Senha é obrigatória.</small>}
                </InputBox>
                
                <Button label="Registrar" icon="pi pi-sign-in" type="submit" className="p-mt-3" />
                <BoxToLogin>
                <VoltarLabel onClick={()=>onLogin()}>Voltar</VoltarLabel>
                </BoxToLogin>
            </FormCustom>
        </>
    );
};


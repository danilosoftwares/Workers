import React, { useState, useRef, FormEvent } from 'react';
import { InputText } from 'primereact/inputtext';
import { Password } from 'primereact/password';
import { Button } from 'primereact/button';
import { Toast } from 'primereact/toast';
import { classNames } from 'primereact/utils';
import { BoxToRegister, FormCustom, InputBox, RegisterLabel } from './styles';
import { LoginComponentProps } from './types';
import { PostLogin } from '../../api/service';

export const LoginComponent: React.FC<LoginComponentProps> = ({onRegister}) => {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [submitted, setSubmitted] = useState<boolean>(false);
    const toast = useRef<Toast>(null);

    const handleLogin = async (e: FormEvent) => {
        e.preventDefault();
        setSubmitted(true);

        const valid = await PostLogin(email,password);

        if (valid.status) {
            localStorage.setItem('auth',JSON.stringify(valid.token));
            localStorage.setItem('user',JSON.stringify(valid.user));
            window.location.reload();
        } else {
            toast.current?.show({ severity: 'error', summary: 'Erro', detail: valid.message, life: 3000 });
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
                <h2>Workers</h2>
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
                        style={{ width: "100%" }}
                    />
                    {submitted && !password && <small className="p-error">Senha é obrigatória.</small>}
                </InputBox>

                <Button label="Login" icon="pi pi-sign-in" type="submit" className="p-mt-3" />

                <BoxToRegister>
                    <label>Não tem login ?</label>
                    <RegisterLabel onClick={()=>onRegister()}>Registre-se</RegisterLabel>
                </BoxToRegister>
            </FormCustom>
        </>
    );
};


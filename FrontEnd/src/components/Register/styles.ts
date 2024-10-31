import styled from "styled-components";


export const FormCustom = styled.form`
    display: flex;
    flex-direction: column;
    gap: 10px;
    padding: 30px;
    width: 350px;
    background-color: rgba(255,255,255,0.5);
    border-radius: 8px;
`

export const InputBox = styled.div`
    display: flex;
    flex-direction: column;
    gap: 5px;
    justify-content: center;
    & input {
        width: 100%;
    }
`

export const BoxToLogin = styled.div`
    display: flex;
    gap: 5px;
    justify-content: center;
`

export const VoltarLabel = styled.label`
    cursor: pointer;
    font-style: normal;
    font-weight: 500;
    font-size: 17px;
    color: black;
`
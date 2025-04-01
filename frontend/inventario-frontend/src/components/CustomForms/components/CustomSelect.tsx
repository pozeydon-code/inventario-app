import { Field, NativeSelect, Select } from "@chakra-ui/react";
import { ReactNode } from "react";
import {
  Control,
  Controller,
  FieldError,
  FieldValues,
  Path,
} from "react-hook-form";

interface Props<T extends FieldValues> {
  name: Path<T>;
  control: Control<T>;
  label: string;
  error?: FieldError;
  isDisabled?: boolean;
  children: ReactNode;
}

export const CustomSelect = <T extends FieldValues>({
  name,
  control,
  label,
  error,
  isDisabled = false,
  children,
}: Props<T>) => {
  return (
    <div>
      <Field.Root>
        <Field.Label>{label}</Field.Label>
        <Controller
          name={name}
          control={control}
          render={({ field }) => (
            <NativeSelect.Root
              id={name}
              {...field}
              disabled={isDisabled}
              className={`form-control ${error ? "is-invalid" : ""}`}
            >
              <NativeSelect.Field placeholder="Seleccione una opcion">
                {children}
              </NativeSelect.Field>
            </NativeSelect.Root>
          )}
        />
        {error && <p className="error">{error.message}</p>}
      </Field.Root>
    </div>
  );
};

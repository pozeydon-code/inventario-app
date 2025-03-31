import { Input, Text } from "@chakra-ui/react";
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
  type?: string;
  error?: FieldError;
  isDisabled?: boolean;
}

export const CustomInput = <T extends FieldValues>({
  name,
  control,
  label,
  type,
  error,
  isDisabled = false,
}: Props<T>) => {
  return (
    <div>
      <label htmlFor={name}>{label}</label>
      <Controller
        name={name}
        control={control}
        render={({ field }) => {
          return (
            <Input
              id={name}
              type={type}
              {...field}
              disabled={isDisabled}
              className={`form-control ${error ? "is-invalid" : ""}`}
              value={
                type === "number"
                  ? (field.value as number)
                  : (field.value as number)
              }
              onChange={field.onChange}
            />
          );
        }}
      />
      {error && <Text color="red.500">{error.message}</Text>}
    </div>
  );
};

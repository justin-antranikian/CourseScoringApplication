import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"

export const dynamic = "force-dynamic"

export default function Home() {
  return (
    <>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead className="flex-1">Invoice</TableHead>
            <TableHead className="flex-1">Status</TableHead>
            <TableHead className="flex-1">Method</TableHead>
            <TableHead className="flex-1">Amount</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          <TableRow>
            <TableCell className="flex-1 font-medium">INV001</TableCell>
            <TableCell className="flex-1">Paid</TableCell>
            <TableCell className="flex-1">Credit Card</TableCell>
            <TableCell className="flex-1">$250.00</TableCell>
          </TableRow>
        </TableBody>
      </Table>
    </>
  )
}
